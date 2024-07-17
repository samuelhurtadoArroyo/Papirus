import { useState } from "react";

const useMyString = (def: string = "") => {
  const [data, setData] = useState<string>(def);

  const resetData = () => setData(def);

  return { data, setData, resetData };
};

export default useMyString;
