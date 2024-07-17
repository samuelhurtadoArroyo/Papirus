"use client"
import { Button } from ".";
import { useFormStatus } from "react-dom";

const SubmitForm = ({
  secondaryId,
  secondaryText,
  primaryId,
  primaryText,
  disableSecondary,
  disablePrimary,
  hideSecondary,
  onClickSecondary
}: {
  secondaryId: string;
  primaryId: string;
  secondaryText: string;
  primaryText: string;
  onClickSecondary: ()=>void;
  disableSecondary?: boolean;
  disablePrimary?: boolean;
  hideSecondary?: boolean;
}) => {
  const { pending } = useFormStatus();

  return (
    <div className="flex w-full justify-between md:justify-end md:gap-[30px] ">
      <Button
        id={secondaryId}
        label={secondaryText}
        type="button"
        onClick={onClickSecondary}
        variant={"secondary"}
        className="px-[42px] uppercase h-10"
        disabled={pending || disableSecondary}
        hidden={hideSecondary}
      />
      <Button
        id={primaryId}
        label={primaryText}
        type="submit"
        variant={"primary"}
        className="px-[42px] uppercase h-10"
        disabled={pending || disablePrimary}
      />
    </div>
  );
};

export default SubmitForm;
