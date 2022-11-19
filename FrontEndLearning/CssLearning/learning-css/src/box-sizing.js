import './box.css';

function BoxSizing() {
  return (
    <main>
      <div class="wrapper">
        <article class="flow">
          <h1>Extrinsic sizing vs intrinsic sizing</h1>
          <figure class="callout">
            <p>Toggle intrinsic sizing on and off to see how you can gain more control with <strong>extrinsic sizing</strong> and let the content have more control with <strong>intrinsic sizing</strong>. </p>
            <p>To see the effect that intrinsic and extrinsic sizing has, go ahead and add a few sentences of content to the card to see the vast differences in display.</p>
          </figure>
          <label class="toggle" for="intrinsic-switch">
            <span class="toggle__label">Turn on intrinsic sizing</span>
            <input type="checkbox" role="switch" class="toggle__element" id="intrinsic-switch" />
            <div class="toggle__decor" aria-hidden="true">
              <div class="toggle__thumb"></div>
            </div>
          </label>
          <div class="box-layout">
            <div class="dimension-label" aria-live="polite" aria-label="Current box width">
              <span data-element="width-label"></span>
            </div>
            <div></div>
            <figure class="box-demo box" data-element="parent-box">
              <img src="http://source.unsplash.com/CiFaYIvZyyA/800" alt="A purple Petunia flower in close focus" />
              <figcaption contenteditable>
                You can edit this text to see how it changes the layout of our box,
                depending on intrinsic and extrinsic sizing.
              </figcaption>
            </figure>
            <div class="dimension-label" aria-live="polite" aria-label="Current box height" data-orientation="vertical">
              <span data-element="height-label"></span>
            </div>
          </div>
        </article>
      </div>
      <article>
        <p>I want to be red and larger than the other text.</p>
        <p>I want to be normal sized and the default color.</p>
      </article>
    </main>
  );
}

export default BoxSizing;
