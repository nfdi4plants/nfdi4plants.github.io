/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{astro,html,js,jsx,md,mdx,svelte,ts,tsx,vue}'],
  theme: {
    extend: {
      colors: {
        transparent: 'transparent',
        current: 'currentColor',
        'black': "#3A3A3A",
        'lightgray': "#ECEBEB",
        'white': "#FEFEFE",
        // https://www.tailwindshades.com/
        'mint': {
          DEFAULT: '#1FC2A7',
          50: '#A8F1E5',
          100: '#96EEE0',
          200: '#73E9D5',
          300: '#50E3CB',
          400: '#2CDDC0',
          500: '#1FC2A7',
          600: '#17927D',
          700: '#106154',
          800: '#08312A',
          900: '#000100',
          950: '#000000'
        },
        'lightblue': {
          DEFAULT: '#4FB3D9',
          50: '#E6F4FA',
          100: '#D5EDF6',
          200: '#B4DEEF',
          300: '#92D0E7',
          400: '#71C1E0',
          500: '#4FB3D9',
          600: '#2B9BC5',
          700: '#217797',
          800: '#175269',
          900: '#0D2E3B',
          950: '#081C24'
        },
        'darkblue': {
          DEFAULT: '#2D3E50',
          50: '#7E99B6',
          100: '#718FAF',
          200: '#597B9E',
          300: '#4A6684',
          400: '#3C526A',
          500: '#2D3E50',
          600: '#19222C',
          700: '#050608',
          800: '#000000',
          900: '#000000',
          950: '#000000'
        },
        'yellow': {
          DEFAULT: '#FFC000',
          50: '#FFEDB8',
          100: '#FFE8A3',
          200: '#FFDE7A',
          300: '#FFD452',
          400: '#FFCA29',
          500: '#FFC000',
          600: '#C79600',
          700: '#8F6C00',
          800: '#574100',
          900: '#1F1700',
          950: '#030200'
        },
        'olive': {
          DEFAULT: '#B4CE82',
          50: '#FFFFFF',
          100: '#FAFCF7',
          200: '#E9F0DA',
          300: '#D7E5BD',
          400: '#C6D99F',
          500: '#B4CE82',
          600: '#9CBE5A',
          700: '#7FA13F',
          800: '#5F782F',
          900: '#3F501F',
          950: '#303C18'
        },
        'red': {
          DEFAULT: '#C21F3A',
          50: '#F1A8B4',
          100: '#EE96A5',
          200: '#E97386',
          300: '#E35068',
          400: '#DD2C4A',
          500: '#C21F3A',
          600: '#92172C',
          700: '#61101D',
          800: '#31080F',
          900: '#010000',
          950: '#000000'
        }
      },
    },
  },
  plugins: [
    require('daisyui'),
    require('@tailwindcss/typography')
  ],
  daisyui: {
    themes: [
      {
        light: {
          ...require("daisyui/src/theming/themes")["light"],
          primary: "#1FC2A7",
          secondary: "#2D3E50",
        },
      },
    ],
    // darkTheme: "dark", // name of one of the included themes for dark mode
    base: true, // applies background color and foreground color for root element by default
    styled: true, // include daisyUI colors and design decisions for all components
    utils: true, // adds responsive and modifier utility classes
    prefix: "", // prefix for daisyUI classnames (components, modifiers and responsive class names. Not colors)
    logs: true, // Shows info about daisyUI version and used config in the console when building your CSS
    themeRoot: ":root", // The element that receives theme color CSS variables
  }
}
