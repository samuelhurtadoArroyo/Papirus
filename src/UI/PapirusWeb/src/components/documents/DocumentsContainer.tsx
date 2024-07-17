import DocumentsButtons from "./DocumentsButtons";
import DocumentsTable from "./DocumentsTable";
import { getGuardianship } from "@/actions/guardianships/getGuardianshipById";
import { getDocuments } from "@/actions/documents/getDocuments";
import { EProcessStatus } from "@/domain/enums/EProcessStatus";

const DocumentsContainer = async ({
  guardianshipId,
}: {
  guardianshipId: number;
}) => {
  const [guardianship, documents] = await Promise.all([
    getGuardianship(guardianshipId),
    getDocuments(guardianshipId),
  ]);
  return (
    <>
      <DocumentsTable
        documents={documents || []}
        guardianshipId={guardianshipId}
      />
      <DocumentsButtons
        documents={documents || []}
        guardianshipId={guardianshipId}
        userId={guardianship?.memberId}
        guardianshipStatusId={
          guardianship?.statusId
            ? guardianship?.statusId
            : EProcessStatus.PendienteAsignacion
        }
      />
    </>
  );
};

export default DocumentsContainer;
