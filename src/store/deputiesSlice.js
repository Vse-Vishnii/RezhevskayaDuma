import { createSlice } from "@reduxjs/toolkit";

const deputiesSlice = createSlice({
  name: "user",
  initialState: { deputies: [], isLoadingDeputies: true },
  reducers: {
    setDeputies(state, action) {
      state.deputies = action.payload;
      state.isLoadingDeputies = false;
    },
  },
});

export const { setDeputies } = deputiesSlice.actions;

export default deputiesSlice.reducer;
