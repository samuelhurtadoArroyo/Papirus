export const authSessionConfig = {
	strategy: (process.env.SESSION_STRATEGY || "jwt" ) as "jwt" | "database" | undefined,
	maxAge: parseInt(process.env.SESSION_MAX_AGE!, 10) || 1 * 60 * 60, // 1 hour in seconds
}