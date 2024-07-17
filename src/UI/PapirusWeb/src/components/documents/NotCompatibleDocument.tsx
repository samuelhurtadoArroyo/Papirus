"use client"
import { textConstants } from "@/domain/globalization/es";
import usePendingState from "@/hooks/usePendingState";
import { downloadUrl } from "@/services/downloadFile";
import { Button } from "../common";

const NotCompatibleDocument = ({
  filePath,
  fileName,
}: {
  filePath: string;
  fileName: string;
}) => {
  const { isPending, setIsPending } = usePendingState();
  const errorMessages = textConstants.components.messages.error;
  const buttonText = textConstants.components.common.download;

  const onDownload = async () => {
    setIsPending(true);
    await downloadUrl(filePath, fileName);
    setIsPending(false);
  };
  return (
    <div className="w-full h-full flex flex-col gap-10 items-center justify-center">
      <p>{errorMessages.fileNotCompatible}</p>
      <Button
        onClick={onDownload}
        id={"download-file"}
        variant="primary"
        label={buttonText}
        className="h-10 uppercase"
        disabled={isPending}
      />
    </div>
  );
};

export default NotCompatibleDocument;
