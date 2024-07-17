import { useState } from "react";

const useMyState = <T = any>(def: T, clearValue = undefined) => {
  const [data, setData] = useState<T>(def);

  const clearData = () => setData(clearData);

  return [data, setData, clearData];
};

export default useMyState;
