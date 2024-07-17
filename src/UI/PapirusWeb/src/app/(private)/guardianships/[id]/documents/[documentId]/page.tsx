import { getDocumentById } from "@/actions/documents/getDocumentById";
import { SubHeader } from "@/components/layout";
import { textConstants } from "@/domain/globalization/es";
import Link from "next/link";
import EmailBody from "@/components/common/EmailBody";
import { getCase } from "@/actions/cases/getCase";
import ViewDocument from "@/components/documents/ViewDocument";

export default async function GuardianshipsDocumentPage({
  params,
}: {
  params: { id: string; documentId: string };
}) {
  const [caseData, document] = await Promise.all([
    getCase(Number(params?.id)),
    getDocumentById(Number(params?.documentId)),
  ]);
  const documentsText = textConstants.pages.documents.header;

  return (
    <>
      <SubHeader title={document?.fileName || ""}>
        <Link
          id="return-btn"
          className="papirus-text-button"
          href={`/guardianships/${params.id}/documents`}>
          {documentsText.button}
        </Link>
      </SubHeader>
      <div className="flex items-start justify-between bg-[--white] h-full w-full rounded-lg shadow-md max-h-[calc(100vh_-_20rem)]">
        {document?.fileName?.endsWith(".txt") ? (
          <EmailBody emailHtmlBody={caseData?.emailHtmlBody || ""} />
        ) : (
          <ViewDocument
            documentPath={document?.filePath || ""}
            documentName={document?.fileName || ""}
          />
        )}
      </div>
    </>
  );
}
