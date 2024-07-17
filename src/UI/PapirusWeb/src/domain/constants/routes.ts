/**
 * An array of routes that are accessible to the public
 * These routes do not require authentication
 * @type {string[]}
 */
export const PUBLIC_ROUTES = ["/privacy-and-policy"];

/**
 * An array of routes that are not accessible to the public
 * These routes require Super User role
 * @type {string[]}
 */
export const LEAD_USER_ONLY_ROUTES = ["/teams"];

/**
 * An array of routes that are not accessible to the public
 * These routes require Administrator role
 * @type {string[]}
 */
export const SUPER_ADMIN_ONLY_ROUTES = [
  "/users",
  "/users/create",
  "/users/[id]/edit",
  "/config",
];

/**
 * An array of routes that are not accessible to the public
 * These routes require authentication
 * @type {string[]}
 */
export const PRIVATE_ROUTES = [
  ...SUPER_ADMIN_ONLY_ROUTES,
  ...LEAD_USER_ONLY_ROUTES,
  "/",
  "/settings",
  "/settings/update-password",
  "/guardianships",
  "/guardianships/[id]",
  "/guardianships/[id]/documents",
  "/guardianships/[id]/documents/[id]",
  "/guardianships/[id]/autoadmit",
  "/guardianships/[id]/email",
  "/guardianships/[id]/resume",
  "/guardianships/[id]/generate/emergency-brief",
  "/guardianships/[id]/generate/response-document",
];

/**
 * An array of routes that are used for authentication
 * These routes will redirect logged in users to /settings
 * @type {string[]}
 */
export const AUTH_ROUTES = [
  "/auth",
  "/auth/signin",
  "/auth/error",
  "/auth/reset",
];

/**
 * An array of routes that are used team management
 * These routes require authentication
 * @type {string[]}
 */
export const TEAMS_ROUTES = ["/teams"];

/**
 * An array of routes that are used for user management
 * These routes require authentication
 * @type {string[]}
 */
export const USERS_ROUTES = ["/users", "/users/create", "/users/[id]/edit"];

/**
 * An array of routes that are used for configuration
 * These routes require authentication
 * @type {string[]}
 */
export const CONFIGS_ROUTES = ["/config"];

/**
 * An array of routes that are used for guardianships management
 * These routes require authentication
 * @type {string[]}
 */
export const GUARDIANSHIPS_ROUTES = [
  "/guardianships",
  "/guardianships/[id]",
  "/guardianships/[id]/documents",
  "/guardianships/[id]/documents/[id]",
  "/guardianships/[id]/extracted-data",
  "/guardianships/[id]/extracted-data/[documentTypeId]",
  "/guardianships/[id]/generated",
];

/**
 * An array of routes that are used for documents management
 * These routes require authentication
 * @type {string[]}
 */
export const DOCUMENTS_ROUTES = [
  "/documents",
  "/documents/[documentId]",
];


/**
 * An array of routes that are used for extracted data
 * These routes require authentication
 * @type {string[]}
 */
export const EXTRACTED_DATA_ROUTES = [
  "/extracted-data",
  "/extracted-data/[documentTypeId]",
  "/extracted-data/resume",
];

/**
 * An array of routes that are used for settings
 * These routes require authentication
 * @type {string[]}
 */
export const SETTINGS_ROUTES = ["/settings", "/settings/update-password"];

/**
 * An array of routes that are used for demands management
 * These routes require authentication
 * @type {string[]}
 */

export const DEMANDS_ROUTES = ["/demands"];

/**
 * The prefix for API authentication routes
 * Routes that start with this prefix are used for API authentication purposes
 * @type {string}
 */
export const API_AUTH_PREFIX = "/api/auth";

/**
 * The default redirect path after logging in
 * @type {string}
 */
export const DEFAULT_LOGIN_REDIRECT = "/";

/**
 * The default redirect path after logging when password must be changed
 * @type {string}
 */
export const DEFAULT_MUST_CHANGE_PASSWORD_REDIRECT = "/update-password";
