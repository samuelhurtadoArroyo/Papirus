import { textConstants } from "@/domain/globalization/es";
import { getProcesses } from "@/services/getProcesses";
import ProcessButton from "./ProcessButton";

export default async function Processes() {
  const procesess = await getProcesses();

  const processesText = textConstants.pages.processes;

  return (
    <section className="grid grid-cols-1 md:grid-cols-3 grid-rows-2 w-full gap-8">
      <h1 id="processes-h1" className="text-black text-2xl aspect-[362/150] font-semibold pr-6 flex flex-col justify-center items-center">
        {processesText.title}
      </h1>
      {procesess.map((process) => (
        <ProcessButton key={process.id} process={process} />
      ))}
    </section>
  );
}
