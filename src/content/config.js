// 1. Import utilities from `astro:content`
import { z, defineCollection, reference } from 'astro:content';
// 2. Define your collection(s)
const newsCollection = defineCollection({ 
  type: 'content',
  schema: ({ image }) => z.object({
    title: z.string(),
    date: z.date(),
    previewText: z.string(),
    image: image().optional(),
  })
});

const eventsCollection = defineCollection({ 
  type: 'content',
  schema: ({ image }) => z.object({
    title: z.string(),
    date: z.date(), 
    excerpt: z.string().max(200),
    category: z.enum(['Conference', 'Hackathon', 'Webinar', 'Training', 'Meeting']), // There might be more
    mode: z.enum(['On-site', 'Online', 'Hybrid']), // There might be more
    audience: z.enum(['Users', 'DataStewards', 'Developers', 'Everyone']).array(), // There might be more
    start: z.date(),
    end: z.date(),
    tutors: z.string().array().optional(),
    image: image().optional(),
    organizer: z.object({
      name: z.string(),
      affiliation: z.string(),
      url: z.string().url()
    }).partial().optional(),
    location: z.object({
      short: z.string().max(50),
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
    subtitle: z.string(),
    tagline: z.string().optional(),
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
    summary: z.string(),
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