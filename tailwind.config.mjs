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
        'mint': "#1FC2A7",
        'lightblue': "#4FB3D9",
        'darkblue': "#2D3E50",
        'yellow': "#FFC000",
        'olive': "#B4CE82",
        'red': "#C21F3A"
      },
    },
  },
  plugins: [
    require('daisyui')
  ],
  daisyui: {
    themes: ["light"], // false: only light + dark | true: all themes | array: specific themes like this ["light", "dark", "cupcake"]
    // darkTheme: "dark", // name of one of the included themes for dark mode
    base: true, // applies background color and foreground color for root element by default
    styled: true, // include daisyUI colors and design decisions for all components
    utils: true, // adds responsive and modifier utility classes
    prefix: "", // prefix for daisyUI classnames (components, modifiers and responsive class names. Not colors)
    logs: true, // Shows info about daisyUI version and used config in the console when building your CSS
    themeRoot: ":root", // The element that receives theme color CSS variables
  }
}
