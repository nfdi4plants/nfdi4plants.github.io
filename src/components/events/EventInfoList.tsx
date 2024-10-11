import { InlineIcon } from '@iconify/react';
import { type ReducedEvent, formatterDate, formatterTime } from '~/util/EventUtil';

interface AdditionalListElements {
  __html: string;
  icon: string;
  ariaLabel?: string;
}

interface Props {
  event: ReducedEvent,
  additional?: (AdditionalListElements | undefined)[];
}

export default function EventInfoList({event, additional}: Props) {
  return (
    <ul className="!pl-0 !mt-0">
      {/* start and end */}
      <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:calendar-time" className="text-xl"/>
        <span>
          {event.data.periodic && <span title="repeating" className="inline-block align-middle"><InlineIcon icon="tabler:infinity"></InlineIcon></span>}
          {formatterDate.format(event.data.when.start) + " " + formatterTime.format(event.data.when.start)}
          <span> – </span>
          {formatterDate.format(event.data.when.end) + " " + formatterTime.format(event.data.when.end)}
        </span>
      </li>
      {/* tutors optional */}
      {event.data.tutors && <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:user" className="text-xl" aria-label="Tutors" />
        <span>
          <b>With </b> {event.data.tutors.map((tutor, index) => <span key={index} dangerouslySetInnerHTML={{__html: index > 0 ? ", " + tutor : tutor}}></span>)}
        </span>
      </li>}
      {/* location */}
      <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:map-pin" className="text-xl" aria-label="Location" />
        <span>
          {
            event.data.location.url 
              ? <a href={event.data.location.url} dangerouslySetInnerHTML={{ __html: event.data.location.short}}></a>
              : <span dangerouslySetInnerHTML={{ __html: event.data.location.short}}></span> 
          }
        </span>
      </li>
      {/* general info: modus, category */}
      <li className="flex items-center gap-2">
        <InlineIcon icon="tabler:info-square-rounded" className="text-xl" aria-label="Info" />
        <span>
          {event.data.category} | {event.data.mode}
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
      {event.data.registration && (event.data.registration.url || event.data.registration.deadline) &&
        <li className="flex items-center gap-2">
          <InlineIcon icon="tabler:clipboard-list" className="text-xl" aria-label="Registration" />
          <span>
            {event.data.registration.url && <a href={event.data.registration.url} className="underline">Register</a>}
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