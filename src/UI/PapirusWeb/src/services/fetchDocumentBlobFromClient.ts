import { FILE_TYPES } from "@/domain/constants/components";

export const fetchDocumentBlobFromClient = async (
  filePath: string,
  fileType?: keyof typeof FILE_TYPES
) => {
  try {
    const response = await fetch(filePath);
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
