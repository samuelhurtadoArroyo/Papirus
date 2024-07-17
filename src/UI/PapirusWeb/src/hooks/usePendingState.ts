import { useState } from "react";

const usePendingState = () => {
  const [isPending, setIsPending] = useState(false);
  return { isPending, setIsPending };
};

export default usePendingState;
