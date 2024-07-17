export default function ErrorForm({ errorMessage }: { errorMessage: string }) {
  return (
    <p id="error-p" className="px-4 py-4 rounded-xl w-full max-w-xl bg-[--error] text-white">
      {errorMessage}
    </p>
  );
}
