import { type ReducedEvent } from "./EventUtil";

export function createEventSchema(reducedEvent: ReducedEvent) {
  return JSON.stringify({
    "@context": "https://schema.org",
    "@type": "Event",
    "name": reducedEvent.data.title,
    "description": reducedEvent.data.description,
    "startDate": reducedEvent.data.when.start,
    "keywords": reducedEvent.data.category,
    "endDate": reducedEvent.data.when.end,
    "eventStatus": "https://schema.org/EventScheduled",
    "eventAttendanceMode": `https://schema.org/${reducedEvent.data.mode}`,
    "performer": [
      reducedEvent.data.tutors?.map((performer) => ({
        "@type": "Person",
        "name": performer,
      }))
    ],
    "audience": {
      "@type": "Audience",
      "audienceType": reducedEvent.data.audience.join(', ')
    },
    "image": reducedEvent.data.image?.src,
    "location": reducedEvent.data.mode === "OnlineEventAttendanceMode" ? {
      "@type": "VirtualLocation",
      "url": reducedEvent.data.location.url,
      "name": reducedEvent.data.location.name
    } : {
      "@type": "Place",
      "name": reducedEvent.data.location.name,
      "address": reducedEvent.data.location.address,
      "url": reducedEvent.data.location.url
    },
    "organizer": {
      "@type": "Person",
      "name": reducedEvent.data.organizer?.name,
      "affiliation": {
        "@type": "Organization",
        "name": reducedEvent.data.organizer?.affiliation
      },
    },
  });
}

interface Article { 
  title: string;
  description: string;
  image?: ImageMetadata
  keywords?: string[];
}

export function createArticleSchema(article: Article) { 
  return JSON.stringify({
    "@context": "https://schema.org",
    "@type": "Article",
    "headline": article.title,
    "description": article.description,
    "image": article.image?.src,
    "keywords": article.keywords?.join(", ")
  })
}