export function safeParse(value: any): number | null {
  if (value === null || value === undefined || value === '') {
    return null;
  }
  const parsed = +value;
  return isNaN(parsed) ? null : parsed;
}
