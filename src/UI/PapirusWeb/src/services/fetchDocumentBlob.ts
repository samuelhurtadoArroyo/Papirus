import { FILE_TYPES } from "@/domain/constants/components";

const baseUrl = process.env.NEXT_PUBLIC_APP_URL;

export const fetchDocumentBlob = async (
  filePath: string,
  fileType?: keyof typeof FILE_TYPES
) => {
  try {
    const response = await fetch(`${baseUrl}api/getDocument`, {
      method: "POST",
      body: JSON.stringify({ filePath }),
    });
    if (response.ok) {
      const blobUrl = response.blob().then(async (blob) => {
        const url = await URL.createObjectURL(
          new Blob([blob], {
            type: fileType
              ? FILE_TYPES[fileType as keyof typeof FILE_TYPES]
              : undefined,
          })
        );
        return url;
      });
      return blobUrl;
    }
    return null;
  } catch (error) {
    console.error("Error during document fetch", error);
    throw error;
  }
};
