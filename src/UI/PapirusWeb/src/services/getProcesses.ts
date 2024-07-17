import { EProcesses } from "@/domain/enums/EProcesses";
import { textConstants } from "@/domain/globalization/es";

export const getProcesses = () => {

  const options = textConstants.pages.processes.options
  
  return [
    {
      id: EProcesses.Demands,
      name: options.demands,
      customerName: "Bancolombia",
      icon: "/demands.svg",
    },
    {
      id: EProcesses.Guardianships,
      name: options.guardianships,
      customerName: "Bancolombia",
      icon: "/guardianships.svg",
      url: "/guardianships",
    },
  ];
};
