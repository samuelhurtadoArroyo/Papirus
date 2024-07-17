import { textConstants } from "@/domain/globalization/es";
import { ReactNode } from "react";

const UserFormLayout = ({children}: {children: ReactNode}) => {
	const usersFormText = textConstants.components.usersForm;
  return (
    <div className="flex flex-col items-center justify-between bg-[--white] w-full rounded-lg shadow-md">
      <header className="flex items-center justify-start px-5 md:px-[30px] w-full h-[47px] bg-[--table-header] rounded-t-lg">
        <h2 className=" font-semibold text-xs">{usersFormText.header}</h2>
      </header>
			{children}
    </div>
  );
};

export default UserFormLayout;
