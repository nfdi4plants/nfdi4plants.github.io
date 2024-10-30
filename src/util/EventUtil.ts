import type { CollectionEntry } from 'astro:content';

type ExtractDateFn<T> = (item: T) => Date;

export function getHumanReadableAttendanceMode(mode: 'OfflineEventAttendanceMode' | 'OnlineEventAttendanceMode' | 'MixedEventAttendanceMode'): string {
  switch (mode) {
    case 'OfflineEventAttendanceMode':
      return 'In-person';
    case 'OnlineEventAttendanceMode':
      return 'Online';
    case 'MixedEventAttendanceMode':
      return 'Hybrid Event';
    default:
      return 'Unknown Event Attendance Mode';
  }
}

export const formatterDate = new Intl.DateTimeFormat('de-DE', { timeZone: 'UTC', day: "2-digit", month: "2-digit", year: "numeric"});
export function formatDateToHref(date: Date): string { 
  return formatterDate
    .formatToParts(date)
    .reduce((accumulator, {type, value}) => { 
      if (type === "literal") {
        return accumulator + "-";
      } else {
        return accumulator + value;
      }
    }, "");
}

// const formatterDate = new Intl.DateTimeFormat('en-US', { day: '2-digit', month: '2-digit', year: 'numeric' });
export const formatterTime = new Intl.DateTimeFormat('de-DE', { timeZone: 'UTC', hour: '2-digit', minute: '2-digit' });

// Extend the type by adding the `periodic` flag
export type ReducedEvent = CollectionEntry<'events'> & {
  data: {
    repeating: boolean; // Add the periodic flag
    when: { start: Date; end: Date } // Update the when field
  };
  href: string; 
};

// Helper function to determine if the date is within the next 31 days
export const isPastOrWithinNext31Days = (date: Date): boolean => {
  const now = new Date();
  const futureLimit = new Date(now.getTime() + 31 * 24 * 60 * 60 * 1000); // 31 days in the future
  return date <= futureLimit;
};

export const reducePeriodicEvent = (event: CollectionEntry<'events'>): ReducedEvent[] => {
  if (Array.isArray(event.data.when)) {
    return event.data.when
      .map((whenEntry) => ({
        ...event,
        data: {
          ...event.data,
          // override the props with specialized information from the specific event date in a series
          ...whenEntry.props, 
          when: whenEntry as { start: Date; end: Date },
          repeating: true
        },
        href: `${event.slug}/${formatDateToHref(whenEntry.start)}`
      }));
  } else {
    return [{
      ...event,
      data: {
        ...event.data,
        when : event.data.when as { start: Date; end: Date },
        repeating: false
      },
      href: event.slug
    }]
  }
}

// Function to reduce periodic events to single events
export const reducePeriodicEvents = (events: CollectionEntry<'events'>[]): ReducedEvent[] => {
  const reducedEvents: ReducedEvent[][] = events.map((event) => reducePeriodicEvent(event));
  return reducedEvents.flat();
};

export function sortByYear<T>(array: T[], extractDate: ExtractDateFn<T>): T[] {
  return array.sort((a, b) => {
    return extractDate(b).getTime() - extractDate(a).getTime();
  });
}

export function groupByYear<T>(array: T[], extractDate: ExtractDateFn<T>): Record<number, T[]> {
  // First, group by year
  const grouped = array.reduce((acc: Record<number, T[]>, obj: T) => {
    const year = extractDate(obj).getFullYear(); // Get the year from the extracted date

    if (!acc[year]) {
      acc[year] = []; // Initialize array for the year if it doesn't exist
    }

    acc[year].push(obj); // Add the object to the group for the corresponding year

    return acc;
  }, {});

  // Sort each group by date
  Object.keys(grouped).forEach((year) => {
    grouped[parseInt(year)] = grouped[parseInt(year)].sort((a, b) => {
      return extractDate(b).getTime() - extractDate(a).getTime();  // Sort by timestamp
    });
  });

  return grouped;
}
