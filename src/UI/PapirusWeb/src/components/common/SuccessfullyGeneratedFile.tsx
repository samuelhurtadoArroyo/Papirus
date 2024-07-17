import { textConstants } from "@/domain/globalization/es";
import Image from "next/image";

const SuccessfullyGeneratedFile = ({ message }: { message: string }) => {
  const altIconText = textConstants.components.alt.icons;
  return (
    <div className="bg-[--white] h-[50vh] w-full flex flex-col items-center justify-center gap-10 rounded-lg shadow-md">
      <p className="text-center font-medium text-xl max-w-96"> {message} </p>
      <Image
        src="/thumb-up.svg"
        alt={altIconText.thumbUp}
        width={74}
        height={76}
      />
    </div>
  );
};

export default SuccessfullyGeneratedFile;
