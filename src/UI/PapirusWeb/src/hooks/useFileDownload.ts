import { CaseProcessDocumentDto } from "@/domain/entities/data-contracts";
import { downloadUrl } from "@/services/downloadFile";
import { useCallback } from "react";

export default function useFileDownload() {
  const automaticDownload = useCallback(
    async ({ path, name }: { path?: string | null; name?: string | null }) => {
      const downloadDocument = async (path: string, name: string) => {
        await downloadUrl(path, name);
      };
      !!path && !!name && (await downloadDocument(path, name));
    },
    []
  );

  const automaticMultipleDownload = useCallback(
    async (files: CaseProcessDocumentDto[]) => {
      async function download_next(i: number) {
        if (i >= files.length) {
          return;
        }
        await automaticDownload({
          path: files[i].filePath,
          name: files[i].fileName,
        });
        setTimeout(async function () {
          await download_next(i + 1);
        }, 1000);
      }
      await download_next(0);
    },
    [automaticDownload]
  );

  return { automaticDownload, automaticMultipleDownload };
}
