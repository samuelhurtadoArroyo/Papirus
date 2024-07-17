export const downloadBlob = async (
  blob: Blob,
  fileName: string,
  fileType: string
) => {
  const url = await URL.createObjectURL(
    new Blob([blob], {
      type: fileType,
    })
  );
  const a = document.createElement("a");
  a.href = url || "";
  a.target = "_parent";
  if ("download" in a) {
    a.download = fileName;
  }
  (document.body || document.documentElement).appendChild(a);
  if (a.click) {
    a.click();
  }
  URL.revokeObjectURL(url);
};

export const downloadUrl = async (url: string, fileName: string) => {
  const a = document.createElement("a");
  a.href = url || "";
  a.target = "_parent";
  if ("download" in a) {
    a.download = fileName;
  }
  (document.body || document.documentElement).appendChild(a);
  if (a.click) {
    a.click();
  }
  URL.revokeObjectURL(url);
};
