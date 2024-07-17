import { useState } from "react";

const useMyBoolean = (def: boolean = false) => {
  const [data, setData] = useState<boolean>(def);

  return {
    isTrue: data,
    isFalse: !data,
    isNotTrue: !data,
    toggle: () => setData(!data),
    setTrue: () => setData(true),
    setFalse: () => setData(false),
    doNothing: () => {},
  };
};

export default useMyBoolean;
