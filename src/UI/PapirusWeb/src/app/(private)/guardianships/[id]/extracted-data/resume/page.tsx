import ScrollToTop from "@/components/common/ScrollToTop";
import SuspenseLoading from "@/components/common/SuspenseLoading";
import ExtractedDataResume from "@/components/extracted-data-resume/ExtractedDataResume";
import { SubHeader } from "@/components/layout";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import { textConstants } from "@/domain/globalization/es";
import Link from "next/link";
import { Suspense } from "react";

const GuardianshipResume = async ({ params }: { params: { id: string } }) => {
  const resumeText = textConstants.pages.extractedDataResume;

  return (
    <>
      <ScrollToTop />
      <SubHeader title={resumeText?.title || ""}>
        <Link
          id="return-btn"
          className="papirus-text-button"
          href={`/guardianships/${params.id}/extracted-data/${
            EGuardianshipDocumentTypes.Email
          }?u=${new Date().getTime()}`}>
          {resumeText.button}
        </Link>
      </SubHeader>
      <Suspense fallback={<SuspenseLoading />}>
        <ExtractedDataResume guardianshipId={Number(params?.id)} />
      </Suspense>
    </>
  );
};

export default GuardianshipResume;
