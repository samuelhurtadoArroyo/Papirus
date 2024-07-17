"use client";
import { EProcesses } from "@/domain/enums/EProcesses";
import { usePermissions } from "@/hooks/usePermissions";
import Image from "next/image";
import { useRouter } from "next/navigation";
const ProcessButton = ({
  process,
  disabled
}: {
  process: {
    id: EProcesses;
    name: string;
    customerName: string;
    icon: string;
    url?: string;
    };
  disabled?: boolean;
}) => {
  const router = useRouter();
  const { validatePermission, permissionConstants } = usePermissions();

  return (
    <button
      id={`${process.id}-btn`}
      key={process.id + process.name}
      onClick={() => (process?.url ? router.push(process?.url) : null)}
      className={`font-medium aspect-[362/150] text-[--white] text-xl leading-8 flex flex-col justify-center items-center rounded-[12px] gap-1 disabled:bg-[--papirus-purple-20] bg-[--papirus-purple]`}
      disabled={
        disabled ||
        !process?.url ||
        (process.id === EProcesses.Demands &&
          !validatePermission(permissionConstants.demands.view)) ||
        (process.id === EProcesses.Guardianships &&
          !validatePermission(permissionConstants.guardianships.view))
      }>
      <Image
        src={process.icon}
        alt={process.name + " icon"}
        height={44}
        width={44}
      />
      <p className="font-medium">{process.name}</p>
    </button>
  );
};

export default ProcessButton;
