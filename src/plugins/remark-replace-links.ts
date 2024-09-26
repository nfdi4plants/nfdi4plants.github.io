import chalk from 'chalk';
import { URLS } from "../statics"
import type { Root } from 'mdast';
import type { Plugin, Transformer } from 'unified';
import {visit} from 'unist-util-visit'

// ANSI escape codes for color
const ANSI_RESET = '\x1b[0m';
const ANSI_YELLOW = '\x1b[33m';

function getUrlFromEnum(key: string): string | null {
  // Check if the input key is a valid key in the URLS enum
  if (key in URLS) {
    // Return the associated value from the enum
    return URLS[key as keyof typeof URLS];
  } else {
    // Return undefined if the key is not found
    return null;
  }
}

function extractTextInsideDoubleCurlyBraces(input: string) {
  // Regular expression to match strings starting with '{{' and ending with '}}'
  const regex = /^\{\{(.+?)\}\}$/;

  // Test if the input matches the pattern
  const match = input.match(regex);

  // If there is a match, return the text inside the curly braces, otherwise return null
  return match ? match[1].trim() : null;
}

// https://dev.to/ritek/the-power-of-remark-6h
export function remarkReplaceLinks(): Plugin<[], Root> {
  const transformer: Transformer<Root> = (tree, file) => {

    visit(tree, 'link', (node) => {
      const key = extractTextInsideDoubleCurlyBraces(node.url);
      if (key) {
        const replacedValue = getUrlFromEnum(key.toUpperCase())
        if (replacedValue) {
          node.url = replacedValue;
        } else {
          console.warn(chalk.yellow(`\t - ⚠️ No URL found for key: "${key}" in file: ${file.path}`));
        };
      }
    });
	};

	return function attacher() {
		return transformer;
	};
}