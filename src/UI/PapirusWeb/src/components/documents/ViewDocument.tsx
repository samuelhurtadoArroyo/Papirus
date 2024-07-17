"use client";
import { FILE_TYPES } from "@/domain/constants/components";
import { useEffect, useState } from "react";
import SuspenseLoading from "../common/SuspenseLoading";
import NotCompatibleDocument from "./NotCompatibleDocument";
import NotFoundDocument from "./NotFoundDocument";
import LoadDocument from "./LoadDocument";
import { getExternalDocument } from "@/actions/documents/getExternalDocument";
import { transformBufferToBlob } from "@/services/transformBufferToBlob";

const ViewDocument = ({
  documentPath,
  documentName,
  className,
}: {
  documentPath: string;
  documentName: string;
  className?: string;
}) => {
  const [filePath, setFilePath] = useState<string | undefined | null>(
    undefined
  );
  const fileType = documentName.toLowerCase().split(".").pop() || "";

  useEffect(() => {
    const fileUrl = async () => {
      const response = await getExternalDocument(documentPath);
      if (response?.arrayBuffer) {
        const url = await transformBufferToBlob({
          arrayBuffer: response.arrayBuffer,
          fileType: fileType as keyof typeof FILE_TYPES,
        });
        setFilePath(url || null);
      }
    };
    fileUrl();
  }, [fileType, documentPath]);

  return (
    <>
      {filePath === undefined && (
        <div className={className || "w-full h-full rounded-lg"}>
          <SuspenseLoading />
        </div>
      )}
      {filePath === null && <NotFoundDocument />}
      {!!filePath && !Object.keys(FILE_TYPES).includes(fileType) && (
        <NotCompatibleDocument filePath={filePath} fileName={documentName} />
      )}
      {!!filePath && Object.keys(FILE_TYPES).includes(fileType) && (
        <LoadDocument
          filePath={filePath}
          fileName={documentName}
          className={className}
        />
      )}
    </>
  );
};

export default ViewDocument;
