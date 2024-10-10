type ExtractDateFn<T> = (item: T) => Date;

export function sortByYear<T>(array: T[], extractDate: ExtractDateFn<T>): T[] {
  return array.sort((a, b) => {
    return extractDate(b).getTime() - extractDate(a).getTime();
  });
}

export function groupByYear<T>(array: T[], extractDate: ExtractDateFn<T>): Record<number, T[]> {
  // First, group by year
  const grouped = array.reduce((acc: Record<number, T[]>, obj: T) => {
    const year = extractDate(obj).getFullYear(); // Get the year from the extracted date

    if (!acc[year]) {
      acc[year] = []; // Initialize array for the year if it doesn't exist
    }

    acc[year].push(obj); // Add the object to the group for the corresponding year

    return acc;
  }, {});

  // Sort each group by date
  Object.keys(grouped).forEach((year) => {
    grouped[parseInt(year)] = grouped[parseInt(year)].sort((a, b) => {
      return extractDate(b).getTime() - extractDate(a).getTime();  // Sort by timestamp
    });
  });

  return grouped;
}
