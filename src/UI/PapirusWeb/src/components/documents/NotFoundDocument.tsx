import { textConstants } from "@/domain/globalization/es";
import React from "react";

const NotFoundDocument = () => {
  const errorMessages = textConstants.components.messages.error;
  return (
    <div className="w-full h-full flex flex-col gap-10 items-center justify-center">
      <p>{errorMessages.fileNotFound}</p>
    </div>
  );
};

export default NotFoundDocument;
