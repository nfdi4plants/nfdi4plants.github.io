// const BASE_PATH = "/arc-website/"
const BASE_PATH = "/"

export enum URLS {
  INTERNAL_HOME = BASE_PATH
}

export const bgColorMap = new Map<string,string> ([
    ['black', "bg-black"],
    ['lightgray', "bg-lightgray"],
    ['white', "bg-white"],
    ['mint', "bg-mint"],
    ['lightblue', "bg-lightblue"],
    ['darkblue', "bg-darkblue"],
    ['yellow', "bg-yellow"],
    ['olive', "bg-olive"],
    ['red', "bg-red"]
])

export const textColorMap = new Map<string,string> ([
    ['black', "text-black"],
    ['lightgray', "text-lightgray"],
    ['white', "text-white"],
    ['mint', "text-mint"],
    ['lightblue', "text-lightblue"],
    ['darkblue', "text-darkblue"],
    ['yellow', "text-yellow"],
    ['olive', "text-olive"],
    ['red', "text-red"]
])