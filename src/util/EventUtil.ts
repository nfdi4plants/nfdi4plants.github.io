import type { CollectionEntry } from 'astro:content';

type ExtractDateFn<T> = (item: T) => Date;

export const formatterDate = new Intl.DateTimeFormat('de-DE', { timeZone: 'Europe/Berlin', year: "numeric", month: "2-digit", day: "2-digit"});
// const formatterDate = new Intl.DateTimeFormat('en-US', { day: '2-digit', month: '2-digit', year: 'numeric' });
export const formatterTime = new Intl.DateTimeFormat('de-DE', { timeZone: 'Europe/Berlin', hour: '2-digit', minute: '2-digit' });

// Extend the type by adding the `periodic` flag
export type ReducedEvent = CollectionEntry<'events'> & {
  slug: string,
  data: {
    periodic: boolean; // Add the periodic flag
    when: { start: Date; end: Date } // Update the when field
  };
};

// Helper function to determine if the date is within the next 31 days
const isPastOrWithinNext31Days = (date: Date): boolean => {
  const now = new Date();
  const futureLimit = new Date(now.getTime() + 31 * 24 * 60 * 60 * 1000); // 31 days in the future
  return date <= futureLimit;
};

// Function to reduce periodic events to single events
export const reducePeriodicEvents = (events: CollectionEntry<'events'>[]): ReducedEvent[] => {
  // this below is in fact a ReducedEvent[][], but ts has issues with slug being a overriden as string in ReducedEvent.
  //@ts-ignore
  const reducedEvent: ReducedEvent[][] = events.map((event) => {
    if (Array.isArray(event.data.when)) {
      return event.data.when
        .filter((whenEntry) => isPastOrWithinNext31Days(whenEntry.start))
        .map((whenEntry) => ({
          ...event,
          data: {
            ...event.data,
            // override the props with specialized information from the specific event date in a series
            ...whenEntry.props, 
            when: whenEntry as { start: Date; end: Date },
            periodic: true
          },
          slug: event.slug + "/" + whenEntry.start.getTime() as string, //generate one file per date
        }));
    } else {
      return [{
        ...event,
        data: {
          ...event.data,
          when : event.data.when as { start: Date; end: Date },
          periodic: false
        },
        slug: event.slug as string,
      }]
    }
  });
  return reducedEvent.flat();
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
