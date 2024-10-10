import type { CollectionEntry } from 'astro:content';
import { sortByYear } from '~/util/GroupByYear';
import { InlineIcon } from '@iconify/react/dist/iconify.js';

interface Props {
  events: CollectionEntry<'events'>[];
}


export default function UpcomingEventBanner({events}: Props) {
  const sortedEvents = sortByYear(events, (event) => new Date(event.data.start));
  //only future events
  const upcomingEvents = sortedEvents.filter((event) => event.data.start > new Date());
  const nextEvent = upcomingEvents[upcomingEvents.length - 1];
  if (!nextEvent) {
    return null;
  }

  return (
    //   <div className="max-w-6xl mx-auto px-4 sm:px-6 py-4 text-md text-center font-medium">
    //     <span className='text-xl flex items-center justify-center'>
    //       <InlineIcon icon="tabler:info-square" className='text-2xl' /> 
    //       <span><b>Upcoming Event: </b> {nextEvent.data.title} ~ {nextEvent.data.start.toLocaleDateString()}</span>
    //     </span>
    //   </div>
    <section
      className="text-sm bg-yellow-300 flex gap-1 overflow-hidden py-2 px-3 relative text-ellipsis whitespace-nowrap items-center"
    >
      <span
        className="bg-white/40 font-semibold px-1 py-0.5 text-xs mr-0.5 rtl:mr-0 rtl:ml-0.5 inline-flex items-center">
          <InlineIcon icon="tabler:info-square" className='text-2xl' />
          UPCOMING EVENT
        </span>
      <a href={"/events/" + nextEvent.slug} className="hover:!underline font-medium">
        <span className='sm:hidden'>More</span>
        <span className='hidden sm:inline'>{nextEvent.data.title} <span className='hidden md:inline'>~ {nextEvent.data.start.toDateString()}</span> </span>
        <span> »</span>
      </a>
    </section>
  );
}