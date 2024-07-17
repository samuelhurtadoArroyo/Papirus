"use client";
import { useState } from "react";

const InputSwitch = ({
  id,
  onToggle,
  defaultValue,
  trueLabel,
  falseLabel,
}: {
  id: string;
  defaultValue: boolean;
  onToggle: () => Promise<void>;
  trueLabel?: string;
  falseLabel?: string;
  width?: number;
  height?: number;
}) => {
  const [checked, setChecked] = useState(defaultValue);

  const handleToggle = async () => {
    setChecked((checked) => !checked);

    // reset toggle if fetch fails
    await onToggle().catch(() => {
      setChecked((checked) => !checked);
    });
  };

  return (
    <label className="flex items-center relative w-max cursor-pointer select-none">
      <input
        id={id}
        type="checkbox"
        className={`appearance-none transition-colors cursor-pointer w-[80px] h-[22px] rounded-full hover:shadow-md focus:shadow-sm bg-[--papirus-grey] checked:bg-[--switch-checked]`}
        checked={checked}
        onChange={(e) => handleToggle()}
      />
      <span
        className={`absolute font-medium text-xs capitalize right-2 text-[--white] ${
          checked && "hidden"
        }`}>
        {falseLabel}
      </span>
      <span
        className={`absolute font-normal text-xs capitalize left-2 text-[--white] ${
          !checked && "hidden"
        }`}>
        {trueLabel}
      </span>
      <span
        className={`w-[18px] h-[18px] ${
          checked ? "right-[2px]" : "left-[2px]"
        } absolute rounded-full transform transition-transform bg-[--white]`}
      />
    </label>
  );
};

export default InputSwitch;
