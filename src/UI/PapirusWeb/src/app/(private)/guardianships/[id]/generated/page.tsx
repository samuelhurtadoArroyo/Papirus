import SuccessfullyGeneratedFile from "@/components/common/SuccessfullyGeneratedFile";
import { textConstants } from "@/domain/globalization/es";
import Link from "next/link";

export default async function SuccesfullyGeneratedDocumentView({}: {
  params: { id: string };
}) {
  const pageinfo = textConstants.pages.generatedDocument;

  return (
    <>
      <SuccessfullyGeneratedFile message={pageinfo.success} />
      <Link
        id="exit-btn"
        href="/guardianships"
        className="papirus-text-button px-10">
        {pageinfo.bottomBtn}
      </Link>
    </>
  );
}
