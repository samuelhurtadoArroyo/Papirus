export const getOnlyDaysDifference = (date: string) => {
	const now = new Date();
	const dateNow = new Date(
    Date.UTC(
      now.getUTCFullYear(),
      now.getUTCMonth(),
      now.getUTCDate()
    )
  );
  const dateToCompare = new Date(
    Date.UTC(
      new Date(date).getUTCFullYear(),
      new Date(date).getUTCMonth(),
      new Date(date).getUTCDate()
    )
	);
	const diff = dateToCompare.getTime() - dateNow.getTime();
	const days = Math.floor(diff / (1000 * 60 * 60 * 24));
	return days;
};
