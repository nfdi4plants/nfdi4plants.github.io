import type { CollectionEntry } from 'astro:content';
import { InlineIcon } from '@iconify/react';

const formatterDate = new Intl.DateTimeFormat('en-US', { year: "numeric", month: "long", day: "numeric"});
// const formatterDate = new Intl.DateTimeFormat('en-US', { day: '2-digit', month: '2-digit', year: 'numeric' });
const formatterTime = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });

interface AdditionalListElements {
  __html: string;
  icon: string;
  ariaLabel?: string;
}

interface Props {
  event: CollectionEntry<'events'>,
  additional?: (AdditionalListElements | undefined)[];
}

export default function EventInfoList({event, additional}: Props) {
  return (
    <ul className="!pl-0 !mt-0">
      {/* start and end */}
      <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:calendar-time" className="text-xl"/>
        <span>
          {formatterDate.format(event.data.start) + " " + formatterTime.format(event.data.start)}
          -
          {formatterDate.format(event.data.end) + " " + formatterTime.format(event.data.end)}
        </span>
      </li>
      {/* tutors optional */}
      {event.data.tutors && <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:user" className="text-xl" aria-label="Tutors" />
        <span>
          <b>With </b> {event.data.tutors.join(', ')}
        </span>
      </li>}
      {/* location */}
      <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:map-pin" className="text-xl" aria-label="Location" />
        <span>
          <span dangerouslySetInnerHTML={{ __html: event.data.location.short}}></span> 
          {event.data.location.url && <sup><a href={event.data.location.url}>More</a></sup>}
        </span>
      </li>
      {/* general info: modus, category */}
      <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:info-square-rounded" className="text-xl" aria-label="Info" />
        <span>
          {event.data.category} - {event.data.mode}
        </span>
      </li>
      {/* audience */}
      <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:users" className="text-xl" aria-label="Target Audience" />
        <span>
          <b>For </b> {event.data.audience.join(', ')}
        </span>
      </li>
      {/* registration */}
      {event.data.registration && event.data.registration.url &&
        <li className="flex items-center gap-2">
          <InlineIcon icon="tabler:clipboard-list" className="text-xl" aria-label="Registration" />
          <span>
            {event.data.registration.url && <a href={event.data.registration.url}>Register</a>}
            {event.data.registration.deadline && <span> until {formatterDate.format(event.data.registration.deadline)}</span>}
          </span>
        </li>
      }
      {additional &&
        additional.filter((info) => info !== undefined).map(({icon, __html, ariaLabel}, index) => (
          <li className="flex items-center gap-2" key={"additional-list-item-" + index}>
            <InlineIcon icon={icon} className="text-xl" aria-label={ariaLabel} />
            <span dangerouslySetInnerHTML={{__html: __html}}>
            </span>
          </li>
        )) 
      } 
    </ul>
  )
}