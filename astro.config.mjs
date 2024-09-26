// @ts-check
import { defineConfig } from 'astro/config';
import rehypeSlug from 'rehype-slug';
import rehypeAutolinkHeadings from 'rehype-autolink-headings';
import {remarkReplaceLinks} from './src/plugins/remark-replace-links';
import tailwind from '@astrojs/tailwind';
import lit from '@astrojs/lit';
import remarkDirective from 'remark-directive';

// https://astro.build/config
export default defineConfig({
  integrations: [tailwind(), lit()],
  markdown: {
    remarkPlugins: [remarkDirective, remarkReplaceLinks()],
    rehypePlugins: [
      rehypeSlug,
      [
        rehypeAutolinkHeadings,
        {
          behavior: 'prepend',
          content: {
            type: 'text',
            value: '#',
          },
          headingProperties: {
            className: ['anchor'],
          },
          properties: {
            className: ['anchor-link'],
          },
        },
      ]
    ]
  },
});