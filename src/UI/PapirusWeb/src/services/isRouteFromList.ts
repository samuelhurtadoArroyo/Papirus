export const isRouteFromList = (routes: string[], route: string) => {
	return routes.includes(route) || routes.includes("/" + route.split("/")[1]);
};