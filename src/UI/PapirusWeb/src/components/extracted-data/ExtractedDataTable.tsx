import { IExtractedDataResume } from "@/domain/interfaces/IExtractedData";
import { Card } from "primereact/card";
import { Montserrat } from "next/font/google";
import { CaseDocumentFieldValueDto } from "@/domain/entities/data-contracts";
import { textConstants } from "@/domain/globalization/es";

const defaultFont = Montserrat({ subsets: ["latin"] });

const ExtractedDataTable = ({
  title,
  data,
}: {
  title: string;
  data: IExtractedDataResume["extractedData"];
}) => {
  const resumeText = textConstants.pages.extractedDataResume;
  const fieldSet = (field: CaseDocumentFieldValueDto, i: number) => {
    const { id, name, fieldValue } = field;
    return (
      <tr key={id && name ? id + name : i}>
        <td className="w-[40%]">
          <p id={`label-${id}-p`} className="font-semibold text-xs">
            {name}
          </p>
        </td>
        <td className="w-[60%]">
          <p id={`value-${id}-p`} className="break-all text-s">
            {fieldValue}
          </p>
        </td>
      </tr>
    );
  };

  return (
    <Card
      className={defaultFont.className}
      title={resumeText.table.title + title}>
      <table className="data-table">
        <thead className="border-b border-b-[--table-border] text-left">
          <tr>
            <th>{resumeText.table.headers.field}</th>
            <th>{resumeText.table.headers.value}</th>
          </tr>
        </thead>
        <tbody>{data?.map(fieldSet)}</tbody>
      </table>
    </Card>
  );
};

export default ExtractedDataTable;
