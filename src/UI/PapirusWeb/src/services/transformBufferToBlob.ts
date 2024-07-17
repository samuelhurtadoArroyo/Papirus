import { FILE_TYPES } from "@/domain/constants/components";

export const transformBufferToBlob = async ({
  arrayBuffer,
  fileType,
  type,
}: {
  arrayBuffer: number[];
  fileType?: keyof typeof FILE_TYPES;
  type?: string;
}) => {
  const binaryData = await Buffer.from(arrayBuffer);

  const imageBase64 = await URL.createObjectURL(
    new Blob([binaryData?.buffer], {
      type:
        type || FILE_TYPES[fileType as keyof typeof FILE_TYPES] || undefined,
    })
  );
  return imageBase64;
};
