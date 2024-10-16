export function safeParse(value: any): number | null {
  const parsed = +value;
  return isNaN(parsed) ? null : parsed;
}
