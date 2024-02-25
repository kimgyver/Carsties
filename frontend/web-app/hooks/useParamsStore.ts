import { create } from "zustand";

type State = {
  pageNumber: number;
  pageSize: number;
  pageCount: number;
  searchTerm: string;
  searchValue: string;
  orderBy: string;
  filterBy: string;
};

type Actions = {
  setParams: (params: Partial<State>) => void;
  reset: () => void;
  setSearch: (value: string) => void;
};

const initialState: State = {
  pageNumber: 1,
  pageSize: 10,
  pageCount: 1,
  searchTerm: "",
  searchValue: "",
  orderBy: "make",
  filterBy: "live"
};

export const useParamsStore = create<State & Actions>()(set => ({
  ...initialState,
  setParams: (newParams: Partial<State>) => {
    set(state => {
      if (newParams.pageNumber) {
        return { ...state, pageNumber: newParams.pageNumber };
      } else {
        return { ...state, ...newParams, pageNumber: 1 };
      }
    });
  },
  reset: () => set(initialState),
  setSearch: (value: string) => set({ searchValue: value })
}));
