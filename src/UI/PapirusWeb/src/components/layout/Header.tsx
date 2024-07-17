import Navbar from "./Navbar";

export default function Header() {
  return (
    <header className="bg-[--papirus-purple] h-20 flex justify-center items-center sticky top-0 z-10">
      <div className="container px-2 max-w-6xl">
        <Navbar />
      </div>
    </header>
  );
}
