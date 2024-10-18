import { safeParse } from './parsing';

describe('safeParse', () => {
  it('should return a number when a valid number string is passed', () => {
    expect(safeParse('42')).toBe(42);
  });

  it('should return null when an invalid number string is passed', () => {
    expect(safeParse('invalid')).toBeNull();
  });

  it('should return the number itself when a number is passed', () => {
    expect(safeParse(42)).toBe(42);
  });

  it('should return null when an empty string is passed', () => {
    expect(safeParse('')).toBeNull();
  });

  it('should return null when null is passed', () => {
    expect(safeParse(null as any)).toBeNull();
  });

  it('should return null when undefined is passed', () => {
    expect(safeParse(undefined)).toBeNull();
  });
});
