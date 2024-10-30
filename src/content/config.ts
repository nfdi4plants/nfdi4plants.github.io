// 1. Import utilities from `astro:content`
import { z, defineCollection, reference } from 'astro:content';
// 2. Define your collection(s)
const newsCollection = defineCollection({ 
  type: 'content',
  schema: ({ image }) => z.object({
    title: z.string(),
    date: z.date(),
    description: z.string(),
    image: image().optional(),
  })
});

const eventsCollection = defineCollection({ 
  type: 'content',
  schema: ({ image }) => z.object({
    title: z.string().trim().min(1),
    description: z.string().trim().max(200).min(1),
    category: z.enum(['Conference', 'Hackathon', 'Webinar', 'Training', 'Meeting']), // There might be more
    mode: z.enum(['OfflineEventAttendanceMode', 'OnlineEventAttendanceMode', 'MixedEventAttendanceMode']), // There might be more
    audience: z.enum(['Users', 'DataStewards', 'Developers']).array(), // There might be more
    when: z.object({
      start: z.date(),
      end: z.date(),
    }).or(z.array(z.object({
      start: z.date(),
      end: z.date(),
      props: z.any().optional()
    }))),
    tutors: z.string().array().optional(),
    image: image().optional(),
    organizer: z.object({
      name: z.string().trim().min(1),
      affiliation: z.string().trim().min(1),
      url: z.string().url()
    }).partial().optional(),
    location: z.object({
      name: z.string().trim().max(50).min(1),
      address: z.string().optional(),
      url: z.string().url().optional(),
    }),
    registration: z.object({
      description: z.string(),
      url: z.string().url(),
      deadline: z.date(),
      seats: z.number(),
    }).partial().optional(),
    external: z.string().url().optional(),
  })
});

const subpageHeroCollection = defineCollection({ 
  type: 'content',
  schema: ({ image }) => z.object({
    title: z.string(),
    description: z.string(),
    tagline: z.string().max(50).optional(),
    image: image(),
    content: z.array(reference('subpageContent')),
    styling: z.object({
      titleColor: z.string().optional(),
      bgColor: z.string().optional(),
      textColor: z.string().optional(),
      textPosition: z.enum(["left", "right", "top", "bottom", "text-only"]).optional(),
    }).optional(),
  })
});

const mainpageCollection = defineCollection({
  type: 'content',
  schema: ({ image }) => z.object({
    title: z.string(),
    index: z.number(),
    subtitle: z.string(),
    image: image(),
    styling: z.object({
      glass: z.boolean(),
      bgColor: z.string(),
      textColor: z.string(),
      buttonColor: z.string(),
      buttonTextColor: z.string(),
      textPosition: z.enum(["left", "right", "top", "bottom", "text-only"]),
    }),
    linkTo: reference('subpage'),
  })
});

// This is level 2 content and is rendered using custom components
const subpageContentCollection = defineCollection({
  type: 'content',
  schema: () => z.object({
    title: z.string(),
    icon: z.string(),
    href: z.string(),
    summary: z.string(),
  })
});

// This is level 3 content in a default markdown render
const articleCollection = defineCollection({
  type: 'content',
  schema: () => z.object({
    title: z.string(),
    description: z.string(),
    tags: z.array(z.string()).optional(),
  })
});

// 3. Export a single `collections` object to register your collection(s)
//    This key should match your collection directory name in "src/content"
export const collections = {
  'news': newsCollection,
  'events': eventsCollection,
  'subpage': subpageHeroCollection,
  'subpageContent': subpageContentCollection,
  'articles': articleCollection,
  'mainpagecards': mainpageCollection,
};