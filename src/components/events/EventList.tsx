import { Fragment, useState } from 'react';
import type { CollectionEntry } from 'astro:content';
import {sortByYear, groupByYear } from '~/util/GroupByYear';
import EventInfoList from './EventInfoList.tsx';

interface Props {
  events: CollectionEntry<'events'>[];
}

interface ConfigState {
  category: string | null;
  mode: string | null;
  audience: string | null;
}

interface ControlButtonProps {
  value: string;
  selectedValue: string | null;
  setValue: () => void;
}

interface ControlButtonsProps {
  label: string;
  options: Set<string>;
  selectedOption: string | null;
  setOption: (option: string) => void;
}


function ControlButton({value, selectedValue, setValue}: ControlButtonProps) {
  const selected = value === selectedValue;
  return (
    <button className={"btn btn-sm " + (selected ? "btn-primary" : "btn-secondary")} onClick={setValue}>
      {value}
    </button>
  )
}

function ControlButtons({label, options, selectedOption, setOption}: ControlButtonsProps) { 
  return (
    <>
      <label>{label}</label>
      <div className="flex flex-row space gap-3">
        {Array.from(options).map((option, index) => (
          <ControlButton value={option} selectedValue={selectedOption} setValue={() => setOption(option)} key={"control-buttons-" + index} />
        ))}
      </div>
    </>
  )
}

function EventCard (event: CollectionEntry<'events'>) {
  return (
    <div className="shadow border p-3 lg:p-5 rounded lg:max-w-5xl xl:max-w-6xl" key={"event-" + event.slug}>
      <div className={"grid lg:grid-rows-1 gap-2 lg:gap-4 lg:grid-cols-2 " + (event.data.image ? 'lg:grid-cols-2' : '')}>
        <div className="prose-sm lg:prose">
          <h1><a href={"/events/" + event.slug}>{event.data.title}</a></h1>
          { event.data.image && <EventInfoList event={event} />}
          <p>
            { event.data.excerpt }
          </p>
        </div>
        {
          event.data.image 
            ? <img src={event.data.image.src} alt={event.data.title} className="rounded max-h-[400px] w-auto m-auto"></img>
            : <div className="prose"><EventInfoList event={event} /></div>
        }
      </div>
    </div>
  )
}

export default function EventList( {events}: Props ) {
  let now = new Date();
  const upcomingEvents = sortByYear(events.filter((event) => {
    return new Date(event.data.start) >= now;
  }), (event) => event.data.start);

  const pastEvents = events.filter((event) => {
    return new Date(event.data.start) < now;
  });
  const pastEventsGrouped = groupByYear(pastEvents, (event) => event.data.start);
  
  const [config, setConfig] = useState<ConfigState>({category: null, mode: null, audience: null});
  // Assuming 'groupedByYear' is the result from the 'groupByYear' function
  const keys = Object.keys(pastEventsGrouped)
    .map(year => parseInt(year)) // Convert keys to numbers
    .sort((a, b) => b - a); // Sort in descending order (b - a)

  const modes = new Set(events.map((event) => event.data.mode));
  const toggleMode = (mode: string) => {
    mode === config.mode ? setConfig({...config, mode: null}) : setConfig({...config, mode: mode}) 
  }
  
  const audiences = new Set(events.flatMap((event) => event.data.audience));
  const toggleAudience = (audience: string) => {
    audience === config.audience ? setConfig({...config, audience: null}) : setConfig({...config, audience: audience}) 
  }

  const categories = new Set(events.flatMap((event) => event.data.category));
  const toggleCategory = (category: string) => {
    category === config.category ? setConfig({...config, category: null}) : setConfig({...config, category: category}) 
  }

  const filterEvents = (event: CollectionEntry<'events'>) => {
    if (config.mode && event.data.mode !== config.mode) return false;
    if (config.audience && !(event.data.audience as string[]).includes(config.audience)) return false;
    if (config.category && event.data.category !== config.category) return false;
    return true;
  }

  const filteredUpcomingEvents = upcomingEvents.filter(filterEvents);

  return (
    <>
      <div className='grid grid-cols-1 md:grid-cols-[auto_minmax(0,_1fr)] gap-x-4 gap-y-2'>
        <ControlButtons label="Mode" options={modes} selectedOption={config.mode} setOption={toggleMode}/>
        <ControlButtons label="Audience" options={audiences} selectedOption={config.audience} setOption={toggleAudience}/>
        <ControlButtons label="Category" options={categories} selectedOption={config.category} setOption={toggleCategory}/>	
      </div>

      <h2 className="text-5xl lg:text-7xl text-center bg-secondary text-secondary-content p-2">Upcoming</h2>
      <div>
        <span className='py-2 text-lg'>
          Found <b>{upcomingEvents.filter(filterEvents).length}</b> upcoming event(s).
        </span>
      </div>
      {
        filteredUpcomingEvents.map((event) => (
          <EventCard {...event} key={"card-" + event.slug} />
        ))
      }
      <h2 className="text-5xl lg:text-7xl text-center bg-secondary text-secondary-content p-2">Archive</h2>
      <div>
        <span className='py-2 text-lg'>
          Found <b>{pastEvents.filter(filterEvents).length}</b> past event(s).
        </span>
      </div>
      {
        keys.map((year) => {
          const events = pastEventsGrouped[year];
          return (
            <div key={year} className="flex flex-col">
              <h1 className="text-5xl lg:text-7xl text-bold self-center mb-2">{year}</h1>
              <div className="grid grid-cols-1 gap-4 lg:gap-10">
                {
                  events.map((event) => (
                    <EventCard {...event} key={"card-" + event.slug}/>
                  ))
                }
              </div>
            </div>
          )
        })
      }
    </>
  );
}