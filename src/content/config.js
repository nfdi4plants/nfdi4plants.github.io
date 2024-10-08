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
    excerpt: z.string(),
    category: z.enum(['Conference', 'Hackathon', 'Webinar', 'Training']), // There might be more
    mode: z.enum(['On-site', 'Online', 'Hybrid']).optional(), // There might be more
    audience: z.enum(['Users', 'Data Stewards', 'Developers']).optional(), // There might be more
    start: z.date(),
    end: z.date(),
    location: z.object({
      description: z.string(),
      organizer: z.string().optional(),
      url: z.string().url().optional(),
      image: image().optional(),
    }),
    registration: z.object({      
      url: z.string().url().optional(),
      deadline: z.date(),
      seats: z.number(),
    }).optional(),
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
      headerColor: z.string().optional(),
      emphasisColor: z.string().optional(),
      textPosition: z.enum(["left", "right", "top", "bottom", "text-only"]).optional(),
    }).optional(),
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
};