import React from "react";
import Image from "next/image";

const LoadDocument = ({
  filePath,
  fileName,
  className,
}: {
  filePath: string;
  fileName: string;
  className?: string;
}) => {
  return (
    <>
      {!fileName.endsWith("jpg") &&
      !fileName.endsWith("jpeg") &&
      !fileName.endsWith("png") ? (
        <iframe
          src={filePath}
          className={className || "w-full h-full rounded-lg"}
          loading="lazy"
          title={fileName || ""}
          name="document-viewer"
        />
      ) : (
        <Image
          src={filePath}
          alt={fileName || ""}
          width={100}
          height={200}
          className={className || "w-full aspect-auto rounded-lg"}
        />
      )}
    </>
  );
};

export default LoadDocument;
