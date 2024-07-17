"use client";
import { Button } from "@/components/common";
import { useFormStatus } from "react-dom";

const SubmitAuthForm = ({
  id,
  text,
  disable,
}: {
  id: string;
  text: string;
  disable?: boolean;
}) => {
  const { pending } = useFormStatus();

  return (
    <div className="flex w-full justify-center">
      <Button
        id={id}
        label={text}
        type="submit"
        variant={"primary"}
        className="w-56 capitalize h-10"
        disabled={pending || disable}
      />
    </div>
  );
};

export default SubmitAuthForm;
