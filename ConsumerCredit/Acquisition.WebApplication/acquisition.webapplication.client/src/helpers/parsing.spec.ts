import { safeParse } from './parsing';

describe('safeParse', () => {
  it('should return a number when a valid number string is passed', () => {
    expect(safeParse('123')).toBe(123);
  });

  it('should return null when an invalid number string is passed', () => {
    expect(safeParse('abc')).toBeNull();
  });

  it('should return null when null is passed', () => {
    expect(safeParse(null)).toBeNull();
  });

  it('should return null when undefined is passed', () => {
    expect(safeParse(undefined)).toBeNull();
  });

  it('should return the number when a number is passed', () => {
    expect(safeParse(456)).toBe(456);
  });

  it('should return the number when a number with leading/trailing spaces is passed', () => {
    expect(safeParse('  789  ')).toBe(789);
  });
});
