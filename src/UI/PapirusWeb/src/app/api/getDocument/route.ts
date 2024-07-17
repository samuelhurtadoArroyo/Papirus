export const POST = async (request: Request) => {
  const res = await request.json();
  const { filePath } = res;
  try {
    const response = await fetch(filePath, {
      method: "GET",
		});
		if (!response.ok) {
			console.error("Failed to fetch document", response.statusText);
			return response;
		}
    const blob = await response.blob();
    return new Response(blob, { status: 200 });
  } catch (error) {
    console.error("Error during document fetch", error);
    return new Response((error as any)?.statusText, { status: 500 });
  }
};
