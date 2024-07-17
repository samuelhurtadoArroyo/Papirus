// A regular expression to check for valid email format
export const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;

// A regular expression to check for at least one special character, one upper case letter, one lower case letter, one number and at least 8 characters
export const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[-_!@#$%^&*]).{8,}$/;