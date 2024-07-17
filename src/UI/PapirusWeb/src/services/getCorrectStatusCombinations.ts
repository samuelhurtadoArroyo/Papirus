import { EProcessStatus } from "@/domain/enums/EProcessStatus";

export const getCorrectStatusCombinations = (
  currentProcessStatusId: number,
  nextProcessStatusId: number
) => {
  return (
    (currentProcessStatusId === EProcessStatus.PendienteAsignacion &&
      nextProcessStatusId === EProcessStatus.Asignada) ||
    (currentProcessStatusId === EProcessStatus.Asignada &&
      nextProcessStatusId === EProcessStatus.EnProgreso) ||
    (currentProcessStatusId === EProcessStatus.EnProgreso &&
      nextProcessStatusId === EProcessStatus.Contestada)
  );
};
