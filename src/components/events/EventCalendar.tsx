import { useState } from 'react';
import { formatterTime, type ReducedEvent } from '~/util/EventUtil.ts';

interface Props {
  events: ReducedEvent[];
}

const weekDays = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];

function getDateKey(date: Date): string {
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${date.getFullYear()}-${month}-${day}`;
}

function getCalendarDays(month: Date): Date[] {
  const firstDay = new Date(month.getFullYear(), month.getMonth(), 1);
  const mondayOffset = (firstDay.getDay() + 6) % 7;
  const daysInMonth = new Date(month.getFullYear(), month.getMonth() + 1, 0).getDate();
  const numberOfDays = Math.ceil((mondayOffset + daysInMonth) / 7) * 7;
  const calendarStart = new Date(month.getFullYear(), month.getMonth(), 1 - mondayOffset);

  return Array.from({ length: numberOfDays }, (_, index) =>
    new Date(calendarStart.getFullYear(), calendarStart.getMonth(), calendarStart.getDate() + index)
  );
}

export default function EventCalendar({ events }: Props) {
  const [displayedMonth, setDisplayedMonth] = useState(() => {
    const today = new Date();
    return new Date(today.getFullYear(), today.getMonth(), 1);
  });
  const todayKey = getDateKey(new Date());
  const monthLabel = new Intl.DateTimeFormat('en-US', { month: 'long', year: 'numeric' }).format(displayedMonth);
  const eventsByDate = new Map<string, ReducedEvent[]>();

  events.forEach((event) => {
    const key = getDateKey(event.data.when.start);
    const eventsForDate = eventsByDate.get(key) ?? [];
    eventsForDate.push(event);
    eventsByDate.set(key, eventsForDate);
  });

  const calendarDays = getCalendarDays(displayedMonth);
  const weeks = Array.from({ length: calendarDays.length / 7 }, (_, index) =>
    calendarDays.slice(index * 7, index * 7 + 7)
  );

  function changeMonth(offset: number) {
    setDisplayedMonth((month) => new Date(month.getFullYear(), month.getMonth() + offset, 1));
  }

  return (
    <section aria-label="Event calendar" className="w-full lg:w-[64rem] xl:w-[72rem] lg:max-w-5xl xl:max-w-6xl">
      <div className="mb-3 grid grid-cols-3 items-center gap-3">
        <button
          type="button"
          className="btn btn-sm btn-secondary"
          aria-label="Previous month"
          onClick={() => changeMonth(-1)}
        >
          &larr;
        </button>
        <div className="text-center">
          <h2 className="mb-1 text-2xl font-bold" aria-live="polite">{monthLabel}</h2>
          <button
            type="button"
            className="btn btn-xs btn-ghost"
            onClick={() => {
              const today = new Date();
              setDisplayedMonth(new Date(today.getFullYear(), today.getMonth(), 1));
            }}
          >
            Today
          </button>
        </div>
        <button
          type="button"
          className="btn btn-sm btn-secondary"
          aria-label="Next month"
          onClick={() => changeMonth(1)}
        >
          &rarr;
        </button>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full table-fixed border-collapse">
          <thead>
            <tr>
              {weekDays.map((day) => (
                <th scope="col" key={day} className="border p-1 text-center text-xs sm:p-2 sm:text-sm">
                  <span className="sm:hidden">{day.slice(0, 2)}</span>
                  <span className="hidden sm:inline">{day}</span>
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {weeks.map((week, weekIndex) => (
              <tr key={`week-${weekIndex}`}>
                {week.map((day) => {
                  const key = getDateKey(day);
                  const isCurrentMonth = day.getMonth() === displayedMonth.getMonth();
                  const isToday = key === todayKey;
                  return (
                    <td
                      key={key}
                      className={`h-24 border p-1 align-top sm:h-32 sm:p-2 ${isCurrentMonth ? '' : 'bg-base-200 text-base-content/50'}`}
                    >
                      <time
                        dateTime={key}
                        aria-current={isToday ? 'date' : undefined}
                        className={`mb-1 inline-flex h-6 min-w-6 items-center justify-center rounded-full px-1 text-sm ${isToday ? 'bg-primary font-bold text-primary-content' : ''}`}
                      >
                        {day.getDate()}
                      </time>
                      <ul className="space-y-1">
                        {(eventsByDate.get(key) ?? []).map((event) => (
                          <li key={event.href}>
                            <a
                              href={`/events/${event.href}`}
                              className="block overflow-hidden rounded bg-secondary px-1 py-0.5 text-xs text-secondary-content hover:underline sm:text-sm"
                              title={`${formatterTime.format(event.data.when.start)} ${event.data.title}`}
                            >
                              <span className="font-semibold">{formatterTime.format(event.data.when.start)}</span>{' '}
                              {event.data.title}
                            </a>
                          </li>
                        ))}
                      </ul>
                    </td>
                  );
                })}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  );
}
